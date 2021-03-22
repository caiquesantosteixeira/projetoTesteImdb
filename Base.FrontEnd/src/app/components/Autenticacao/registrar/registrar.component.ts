import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { ErroService } from './../../../shared/services/erro.service';

declare var $: any;

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.scss']
})
export class RegistrarComponent implements OnInit {
  data: Date = new Date();
  Login: any = {};
  dadosForm: FormGroup;
  dadosEmp: any = {};

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private erroService: ErroService,
    private router: Router
    ) { }

  checkFullPageBackgroundImage() {
      const $page = $('.full-page');
      const imageSrc = $page.data('image');

      if(imageSrc !== undefined){
          const imageContainer = '<div class="full-page-background" style="background-image: url(' + imageSrc + ') "/>';
          $page.append(imageContainer);
      }
  }

  ngOnInit() {
    this.checkFullPageBackgroundImage();
    setTimeout(function(){
        // after 1000 ms we add the class animated to the login/register card
        $('.card').removeClass('card-hidden');
    }, 100 );

    this.formulario();
    const empresas = JSON.parse(localStorage.getItem('SGFempresas'));
    if(empresas !== null){
      this.dadosEmp.razaoSocial = empresas[0].razaoSocial;
      this.dadosEmp.cnpj = empresas[0].cnpjCpf;
    }

  }

  formulario(){
    this.dadosForm = this.fb.group({
     registro: ['', [Validators.required, Validators.minLength(29)]]
    });
  }
}
