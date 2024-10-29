import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CounterComponent } from './counter.component';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip'
@NgModule({
  declarations: [
    CounterComponent
  ],
  imports: [
    CommonModule,
    MatButtonModule,
    MatTooltipModule
  ],
  exports: [CounterComponent]
})
export class CounterModule { }
