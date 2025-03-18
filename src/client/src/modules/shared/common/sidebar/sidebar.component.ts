import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faAngleDoubleLeft, faAngleDoubleRight, faCaretDown, faDashboard, faDoorOpen, faFilm, faGear, faList, faPercent, faTicketSimple, faUser, faUserShield } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-sidebar',
  imports: [FontAwesomeModule, CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  //#region Font Awesome Icons
  public faDashboard: IconDefinition = faDashboard;
  public faList: IconDefinition = faList;
  public faGear: IconDefinition = faGear;
  public faAngleDoubleLeft: IconDefinition = faAngleDoubleLeft;
  public faAngleDoubleRight: IconDefinition = faAngleDoubleRight;
  public faUser: IconDefinition = faUser;
  public faUserShield: IconDefinition = faUserShield;
  public faTicketSimple: IconDefinition = faTicketSimple;
  public faFilm: IconDefinition = faFilm;
  public faDoorOpen: IconDefinition = faDoorOpen;
  public faPercent: IconDefinition = faPercent;
  public faCaretDown: IconDefinition = faCaretDown;


  //#endregion
  public isShowSidebar: boolean = false;
  public isUserDropdownOpen: boolean = false;
  public isTicketDropdownOpen: boolean = false;
}
