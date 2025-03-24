import { CommonModule } from '@angular/common';
import { Component, HostListener, Inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ModalService } from '../../../../services/modal.service';
import {
  faSortDesc,
  faUser,
  faUserCircle,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
import { AUTH_SERVICE } from '../../../../constants/injection.constant';
import { IAuthService } from '../../../../services/auth/auth-service.interface';

@Component({
  selector: 'app-header',
  imports: [CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  constructor(
    private modalService: ModalService,
    @Inject(AUTH_SERVICE) private authService: IAuthService
  ) {
    this.authService.isAuthenticated().subscribe((res) => {
      this.isAuthenticated = res;
    });
  }

  public faUser: IconDefinition = faUserCircle;

  public faDropDown: IconDefinition = faSortDesc;
  public isAuthenticated: boolean = false;
  // Show form login
  openLogin() {
    this.modalService.open('login');
  }

  // Show form register
  openRegister() {
    this.modalService.open('register');
  }
  // Mặc định bgheader không mờ
  bgHeaderOpacity: number = 1;
  isDropdownOpen = false;

  // Đóng/mở dropdown menu
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  public logout(): void {
    this.authService.logout();
  }

  // Header sẽ mờ dần khi cuộn xuống, không nhỏ hơn 0.7
  @HostListener('window:scroll', [])
  onWindowScroll() {
    const scrollTop = window.scrollY || document.documentElement.scrollTop;
    this.bgHeaderOpacity = Math.max(1 - scrollTop / 500, 0.7);
    // Chỉ làm mờ nền xuống tối thiểu 0.7, không ảnh hưởng nội dung
  }
}
