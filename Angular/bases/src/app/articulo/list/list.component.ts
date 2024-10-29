import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  articulosLst: string[] = ['Batería', 'Filtro', 'Garrafa de Aceite'];
  artEliminado: string;
  
  constructor() { }

  ngOnInit(): void {
  }

  eliminaUltArt() {
    this.artEliminado = this.articulosLst.pop();
    console.log(this.artEliminado);
  }

  reset() {
    this.articulosLst = ['Batería', 'Filtro', 'Garrafa de Aceite'];
    this.artEliminado = null;
  }

}
