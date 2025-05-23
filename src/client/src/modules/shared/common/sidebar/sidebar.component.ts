import { CommonModule } from '@angular/common';
import { Component, HostListener, Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faAngleDoubleLeft,
  faAngleDoubleRight,
  faCaretDown,
  faDashboard,
  faDoorOpen,
  faFilm,
  faGear,
  faList,
  faPercent,
  faSignOut,
  faTicketSimple,
  faUserAlt,
  faUserShield,
} from '@fortawesome/free-solid-svg-icons';
import { AUTH_SERVICE } from '../../../../constants/injection.constant';
import { IAuthService } from '../../../../services/auth/auth-service.interface';
import { UserInformation } from '../../../../models/auth/user-information.model';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  constructor(
    private readonly router: Router,
    @Inject(AUTH_SERVICE) private authService: IAuthService
  ) {
    this.authService.isAuthenticated().subscribe((res) => {
      this.isAuthenticated = res;
    });

    this.authService.getUserInformation().subscribe((res) => {
      if (res) {
        this.currentUser = res;
      }
    });
  }

  public isActive(route: string): boolean {
    return this.router.url === route; // Kiểm tra trang hiện tại
  }
  public isPermitted(): boolean {
    return this.authService.hasAnyRole(['Admin']);
  }

  //#region Font Awesome Icons
  public faDashboard: IconDefinition = faDashboard;
  public faList: IconDefinition = faList;
  public faGear: IconDefinition = faGear;
  public faAngleDoubleLeft: IconDefinition = faAngleDoubleLeft;
  public faAngleDoubleRight: IconDefinition = faAngleDoubleRight;
  public faUser: IconDefinition = faUserAlt;
  public faUserShield: IconDefinition = faUserShield;
  public faTicketSimple: IconDefinition = faTicketSimple;
  public faFilm: IconDefinition = faFilm;
  public faDoorOpen: IconDefinition = faDoorOpen;
  public faPercent: IconDefinition = faPercent;
  public faCaretDown: IconDefinition = faCaretDown;
  public faSignOut: IconDefinition = faSignOut;

  public isAuthenticated: boolean = false;
  public currentUser: UserInformation | null = null;
  ngOnInit(): void {
    this.checkScreenSize(); // Kiểm tra kích thước màn hình khi component khởi tạo
    this.restoreDropdownState(); // Khôi phục trạng thái dropdown từ localStorage
    this.userRoles =
      this.authService.getUserInformationFromAccessToken()?.roles || [];
  }

  private restoreDropdownState() {
    const userDropdownState = localStorage.getItem('isUserDropdownOpen');
    const ticketDropdownState = localStorage.getItem('isTicketDropdownOpen');

    this.isUserDropdownOpen = userDropdownState === 'true';
    this.isTicketDropdownOpen = ticketDropdownState === 'true';
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.checkScreenSize();
  }

  private checkScreenSize() {
    if (window.innerWidth < 768) {
      this.isShowSidebar = false; // Tự động thu nhỏ khi màn hình nhỏ hơn 768px
    } else {
      this.isShowSidebar = true;
    }
  }

  toggleUserDropdown() {
    this.isUserDropdownOpen = !this.isUserDropdownOpen;
    localStorage.setItem('isUserDropdownOpen', String(this.isUserDropdownOpen));
  }

  toggleTicketDropdown() {
    this.isTicketDropdownOpen = !this.isTicketDropdownOpen;
    localStorage.setItem(
      'isTicketDropdownOpen',
      String(this.isTicketDropdownOpen)
    );
  }

  public logout(): void {
    this.authService.logout();
    setTimeout(() => {
      window.location.reload();
    }, 500);
  }

  //#endregion
  public isShowSidebar: boolean = true;
  public isUserDropdownOpen: boolean = false;
  public isTicketDropdownOpen: boolean = false;
  public userRoles: string[] = [];
}
