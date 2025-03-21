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
import { TicketlistComponent } from './ticketlist/ticketlist.component';
import { TicketconfirmComponent } from './ticketconfirm/ticketconfirm.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'customermanagement', component: CustomermanagementComponent },
  { path: 'employeemanagement', component: EmployeemanagementComponent },
  { path: 'moviemanagement', component: MoviemanagementComponent },
  { path: 'promotionmanagement', component: PromotionmanagementComponent },
  { path: 'roommanagement', component: RoommanagementComponent },
  { path: 'ticketselling', component: TicketsellingComponent },
  { path: 'ticketlist', component: TicketlistComponent },
  { path: 'setting', component: SettingComponent },
  { path: 'ticketconfirm', component: TicketconfirmComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ]
})
export class AdminModule { }
