import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from '../../common/footer/footer.component';
import { HeaderComponent } from '../../common/header/header.component';
import { LoginComponent } from '../../../auth/login/login.component';
import { RegisterComponent } from '../../../auth/register/register.component';

@Component({
  selector: 'app-customer-layout',
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
  ],
  templateUrl: './customer-layout.component.html',
  styleUrl: './customer-layout.component.css',
})
export class CustomerLayoutComponent {}
