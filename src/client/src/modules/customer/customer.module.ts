import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShowtimeComponent } from './showtime/showtime.component';
import { TicketpriceComponent } from './ticketprice/ticketprice.component';
import { PromotionComponent } from './promotion/promotion.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'promotion', component: PromotionComponent },
  { path: 'showtime', component: ShowtimeComponent },
  { path: 'ticketprice', component: TicketpriceComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ]
})
export class CustomerModule { }
