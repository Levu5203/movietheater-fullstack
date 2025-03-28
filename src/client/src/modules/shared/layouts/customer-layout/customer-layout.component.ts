import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from '../../common/footer/footer.component';
import { HeaderComponent } from '../../common/header/header.component';
import { LoginComponent } from '../../../auth/login/login.component';
import { RegisterComponent } from '../../../auth/register/register.component';
import { ChangePasswordComponent } from '../../../customer/profile/change-password/change-password.component';
import { ModalService } from '../../../../services/modal.service';
import { CommonModule } from '@angular/common';
import { ForgotPasswordComponent } from '../../../auth/forgotpassword/forgotpassword.component';

@Component({
  selector: 'app-customer-layout',
  imports: [
    CommonModule,
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
    ChangePasswordComponent,
    ForgotPasswordComponent,
  ],
  templateUrl: './customer-layout.component.html',
  styleUrl: './customer-layout.component.css',
})
export class CustomerLayoutComponent {
  constructor(public modalService: ModalService) {}
}
