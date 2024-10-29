import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticuloComponent } from './articulo.component';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    ArticuloComponent
  ],
  imports: [
    CommonModule,
    MatButtonModule
  ],
  exports:[ArticuloComponent]
})
export class ArticuloModule { }
