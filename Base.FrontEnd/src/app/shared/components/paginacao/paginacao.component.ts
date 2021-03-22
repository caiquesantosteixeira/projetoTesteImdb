import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, OnChanges, OnDestroy } from '@angular/core';
import { PagerService } from '../../services/paginacao.service';
import { PaginacaoService } from './paginacao.service';
import { ErroService } from './../../services/erro.service';

@Component({
  selector: 'app-paginacao',
  templateUrl: './paginacao.component.html',
  styleUrls: ['./paginacao.component.scss']
})
export class PaginacaoComponent implements OnChanges, OnInit, OnDestroy {

  // Propriedades da paginação
  @Input() public pagina: any = {};
  @Input() public EndPoint = '';
  @Input() public registroExcluido = false;
  @Input() public filtroSelecionado = '';
  @Input() public filtroValue = '';
  @Input() public qtdRegPagina = 15;
  @Input() public execPesqVazia = false;
  @Input() public id = 0;
  @Output() returnDados = new EventEmitter();

  public pagedItems: number;
  private listaVazia = true;
  private totalItens: number;
  public paginaAtual: number;
  public SemRegistros = false;
  public listarBusca = false;

  constructor(
    private pagerService: PagerService,
    private paginacaoService: PaginacaoService,
    private erroService: ErroService
  ) { }

  ngOnInit() {

    // Desabilitado para nao ser executado mais de 1 vez
    // this.getPageServico(this.EndPoint, 1, this.qtdRegPagina, '', '', this.id);
  }

  ngOnChanges(changes: SimpleChanges) {
    // console.log('alteracao', changes);
    const _REG_EXCLUIDO = changes.registroExcluido === undefined ? false : changes.registroExcluido.currentValue;
    const _FILTRO_VALUE_ATUAL = changes.filtroValue === undefined ? '' : changes.filtroValue.currentValue;
    const _FILTRO_VALUE_ANTERIOR = changes.filtroValue === undefined ? '' : changes.filtroValue.previousValue;
    const _FILTRO_SEL = changes.filtroSelecionado === undefined ? this.filtroSelecionado : changes.filtroSelecionado.currentValue;

    if (((_FILTRO_VALUE_ATUAL.length) > 0 || _FILTRO_VALUE_ATUAL === '')
      && _FILTRO_VALUE_ATUAL !== _FILTRO_VALUE_ANTERIOR
      || this.execPesqVazia === true
    ) {
      this.listaVazia = true;
      this.getPageServico(this.EndPoint, 1, this.qtdRegPagina, _FILTRO_SEL, _FILTRO_VALUE_ATUAL, this.id);
    }

    if (_REG_EXCLUIDO) {
      this.listaVazia = true;
      this.getPageServico(this.EndPoint, 1, this.qtdRegPagina, '', '', this.id);
    }

    this.execPesqVazia = false;
  }

  // Controla o fluxo da paginação
  public setPage(pagina: number, acao?: string) {
    if (pagina < 1 || pagina > this.totalItens) {
      return;
    }

    this.pagina = this.pagerService.getPager(this.totalItens, pagina, this.qtdRegPagina);
    this.paginaAtual = pagina;
    // avanca e busca de acordo com os parametros
    if (this.listarBusca === true && acao !== undefined) {
      this.getPageServico(this.EndPoint, pagina, this.qtdRegPagina, this.filtroSelecionado, this.filtroValue, this.id);
    } else if (this.listarBusca === false && acao !== undefined) {
      this.getPageServico(this.EndPoint, pagina, this.qtdRegPagina, '', '', this.id);
    }

    // pega a página atual de itens
    this.pagedItems = 1;
  }

  /// Responsavel pela busca/exibicao dos dados paginados
  private getPageServico(EndPoint: string, pgAtual: number, QtdregPaginas: number, ftl?: string, ValueFiltro?: string, id = 0) {
    pgAtual = pgAtual === 0 ? 1 : pgAtual;
    QtdregPaginas = QtdregPaginas === 0 ? 15 : QtdregPaginas;

    this.paginacaoService.getRegistrosPage(EndPoint, pgAtual, QtdregPaginas, ftl, ValueFiltro, id)
      .subscribe((a: any[]) => {
        if (a[0] !== undefined) {
          if (a[0].lista.length === 0) {
            this.SemRegistros = true;
          } else { this.SemRegistros = false; }
          this.returnDados.emit({ dados: a[0].lista });
          this.totalItens = a[0].total;
          // Só pode ser executado apenas uma vez
          if (this.listaVazia) {
            this.setPage(1);
            this.listaVazia = false;
          }
        } else {
          console.log('Lista de usuário vazia');
        }
      }, error => this.erroService.setError(error));
  }

  ngOnDestroy(): void {
    this.execPesqVazia = null;
  }

}
