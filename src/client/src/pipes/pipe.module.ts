import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomFormatPipe } from './custom-format.pipe';

@NgModule({
  exports: [CustomFormatPipe],
  imports: [CommonModule, CustomFormatPipe],
})
export class PipesModule {}
