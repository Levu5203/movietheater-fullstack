import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShowtimeComponent } from './showtime/showtime.component';
import { TicketpriceComponent } from './ticketprice/ticketprice.component';
import { PromotionComponent } from './promotion/promotion.component';
import { SeatshowtimeComponent } from './seatshowtime/seatshowtime.component';
import { BookingComponent } from './booking/booking.component';
import { MoviedetailComponent } from './moviedetail/moviedetail.component';
import { ProfileComponent } from './profile/profile.component';
import { ResetPasswordComponent } from '../auth/resetpassword/resetpassword.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'promotion', component: PromotionComponent },
  { path: 'showtime', component: ShowtimeComponent },
  { path: 'ticketprice', component: TicketpriceComponent },
  { path: 'seatshowtime', component: SeatshowtimeComponent },
  { path: 'bookingmanagement', component: BookingComponent },
  { path: 'moviedetail', component: MoviedetailComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'booking', component: BookingComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class CustomerModule {}