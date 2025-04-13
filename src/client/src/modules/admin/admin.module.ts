import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CustomermanagementComponent } from './customermanagement/customermanagement.component';
import { EmployeemanagementComponent } from './employeemanagement/employee-list/employee-list.component';
import { MoviemanagementComponent } from './moviemanagement/moviemanagement.component';
import { PromotionmanagementComponent } from './promotionmanagement/promotionmanagement.component';
import { RoommanagementComponent } from './roommanagement/room-list/room-list.component';
import { SettingComponent } from './setting/setting.component';
import { RouterModule, Routes } from '@angular/router';
import { TicketsellingComponent } from './ticketselling/ticketselling.component';
import { BookingManagementComponent } from './bookingmanagement/bookingmanagement.component';
import { TicketconfirmComponent } from './ticketconfirm/ticketconfirm.component';
import { AddmovieComponent } from './addmovie/addmovie.component';
import { AddpromotionComponent } from './addpromotion/addpromotion.component';
import { UpdatepromotionComponent } from './updatepromotion/updatepromotion.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { TiketsellingSelectseatComponent } from './ticketselling/tiketselling-selectseat/tiketselling-selectseat.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'editprofile', component: EditProfileComponent },
  { path: 'customermanagement', component: CustomermanagementComponent },
  { path: 'employeemanagement', component: EmployeemanagementComponent },
  { path: 'moviemanagement', component: MoviemanagementComponent },
  { path: 'promotionmanagement', component: PromotionmanagementComponent },
  { path: 'rooms', component: RoommanagementComponent },
  { path: 'ticketselling', component: TicketsellingComponent },
  { path: 'bookingmanagement', component: BookingManagementComponent },
  { path: 'setting', component: SettingComponent },
  { path: 'ticketconfirm', component: TicketconfirmComponent },
  { path: 'addmovie', component: AddmovieComponent },
  { path: 'addpromotion', component: AddpromotionComponent },
  { path: 'updatepromotion/:id', component: UpdatepromotionComponent },
  { path: 'updatepromotion', component: UpdatepromotionComponent },
  { path: 'ticketselling-selectseat', component: TiketsellingSelectseatComponent },

  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class AdminModule {}
