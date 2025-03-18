import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FontAwesomeModule,
} from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-header',
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})

export class HeaderComponent {
  isLoggedIn = false;  // Trạng thái đăng nhập
  userName = '';       // Lưu tên user sau khi đăng nhập
  isDropdownOpen = false;

  // Giả lập đăng nhập
  signin() {
    this.isLoggedIn = true;
    this.userName = 'John Doe'; // Thay thế bằng user động khi có backend

    //signin logic
  }

  // Đóng/mở dropdown menu
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  // Giả lập đăng xuất
  logout() {
    this.isLoggedIn = false;
    this.userName = '';
    this.isDropdownOpen = false;
  }
}
