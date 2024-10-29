import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.css']
})
export class CounterComponent implements OnInit {

  contador: number = 10;
  constructor() { }

  ngOnInit(): void {
  }

  incrementar(value: number) {
    this.contador += value;
  }

  decrementar(value: number) {
    this.contador -= value;
  }

  reset() {
    this.contador = 10;
  }

}
