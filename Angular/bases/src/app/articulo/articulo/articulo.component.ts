import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-articulo',
  templateUrl: './articulo.component.html',
  styleUrls: ['./articulo.component.css']
})
export class ArticuloComponent implements OnInit {

  nombre: string = 'Batería';
  precio: number = 1200;

  nombreChanged: boolean = false;
  precioChanged: boolean = false;
  
  constructor() { }

  ngOnInit(): void {
  }

  getArticuloInfo(): string {
    return `${this.nombre} - ${this.precio}`
  }

  cambiarPrecio(){
    this.precio = 250;
    this.precioChanged = true;
  }

  cambiarNombre() {
    this.nombre = 'Filtro de Aire';
    this.nombreChanged = true;
  }

  resetForm() {
    this.nombre = 'Batería';
    this.precio = 1200;
    this.nombreChanged = this.precioChanged = false;
  }

}
