import { Component } from '@angular/core';
import { SidebarComponent } from '../../common/sidebar/sidebar.component';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ChangePasswordComponent } from '../../../customer/profile/change-password/change-password.component';
import { ModalService } from '../../../../services/modal.service';

@Component({
  selector: 'app-admin-layout',
  imports: [
    RouterOutlet,
    SidebarComponent,
    CommonModule,
    ChangePasswordComponent,
  ],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.css',
})
export class AdminLayoutComponent {
  constructor(public modalService: ModalService) {}
}
