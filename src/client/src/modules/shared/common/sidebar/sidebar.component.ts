import { CommonModule } from '@angular/common';
import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faAngleDoubleLeft, faAngleDoubleRight, faCaretDown, faDashboard, faDoorOpen, faFilm, faGear, faList, faPercent, faTicketSimple, faUser, faUserAlt, faUserLarge, faUserShield } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-sidebar',
  imports: [FontAwesomeModule, CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  constructor(private router: Router){

  }

  public isActive(route: string): boolean {
    return this.router.url === route; // Kiểm tra trang hiện tại
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

  ngOnInit(): void {
    this.checkScreenSize(); // Kiểm tra kích thước màn hình khi component khởi tạo
    this.restoreDropdownState(); // Khôi phục trạng thái dropdown từ localStorage
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
      this.isShowSidebar = false; // Tự động thu nhỏ khi màn hình nhỏ hơn 1024px
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
    localStorage.setItem('isTicketDropdownOpen', String(this.isTicketDropdownOpen));
  }

  //#endregion
  public isShowSidebar: boolean = true;
  public isUserDropdownOpen: boolean = false;
  public isTicketDropdownOpen: boolean = false;
  
}
