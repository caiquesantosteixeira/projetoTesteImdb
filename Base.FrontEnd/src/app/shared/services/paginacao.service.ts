import { Injectable } from '@angular/core';
import { range } from 'rxjs';

// import * as _ from 'underscore';

@Injectable({
  providedIn: 'root'
})
export class PagerService {
getPager(totalItems: number, currentPage: number = 1, pageSize: number = 15) {
// calculando o total de paginas

        const totalPages = Math.ceil(totalItems / pageSize);
        let startPage: number;
        let endPage: number;
        if (totalPages <= 10) {
            // Se tiver menos de 10 páginas totais para mostrar tudo
            startPage = 1;
            endPage = totalPages;
        } else {
            // Se tiver mais de 10 páginas totais, calcular as páginas inicial e final
            if (currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;
            } else {
                startPage = currentPage - 5;
                endPage = currentPage + 4;
            }
        }

        // Cálculo de índices de itens iniciais e finais
        const startIndex = (currentPage - 1) * pageSize;
        const endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

        // Criar um array de páginas para o ng-repeat no componente
        // const pages = range(startPage, endPage + 1);
        const pages: any[] = [];
        for (let i = startPage; i <= endPage; i++) {
          pages.push(i);
        }
        // Retornar objeto com todas as propriedades exigidas para exibição
        return {
            totalItems: totalItems,
            currentPage: currentPage,
            pageSize: pageSize,
            totalPages: totalPages,
            startPage: startPage,
            endPage: endPage,
            startIndex: startIndex,
            endIndex: endIndex,
            pages: pages
        };
    }

}
