import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list.component';
import { MatListModule } from '@angular/material/list'
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    MatListModule,
    MatButtonModule
  ],
  exports:[ListComponent]
})
export class ListModule { }
