import { CommonModule } from '@angular/common';
import { Component, HostListener, Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ModalService } from '../../../../services/modal.service';
import {
  faBars,
  faSortDesc,
  faUserCircle,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
import { AUTH_SERVICE } from '../../../../constants/injection.constant';
import { IAuthService } from '../../../../services/auth/auth-service.interface';
import { UserInformation } from '../../../../models/auth/user-information.model';

@Component({
  selector: 'app-header',
  imports: [CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  constructor(
    private readonly modalService: ModalService,
    @Inject(AUTH_SERVICE) private readonly authService: IAuthService,
    private readonly router: Router // Inject Router
  ) {
    this.authService.isAuthenticated().subscribe((res) => {
      if (this.authService.isTokenExpired()) {
        this.authService.logout();
        return;
      }
      this.isAuthenticated = res;
    });

    this.authService.getUserInformation().subscribe((res) => {
      if (res) {
        this.currentUser = res;
        this.hasPermission = this.authService.hasAnyRole(['Admin', 'Employee']);
      }
    });
  }

  public isActive(route: string): boolean {
    return this.router.url === route; // Kiểm tra trang hiện tại
  }

  public faUser: IconDefinition = faUserCircle;
  public faBars: IconDefinition = faBars;

  public faDropDown: IconDefinition = faSortDesc;
  public isMobileMenuOpen = false;
  public isAuthenticated: boolean = false;
  public currentUser: UserInformation | null = null;
  public hasPermission: boolean = false;

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

  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
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
