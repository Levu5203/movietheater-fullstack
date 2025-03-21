import { CommonModule } from '@angular/common';
import { Component, HostListener } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-header',
  imports: [CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  // Mặc định bgheader không mờ
  bgHeaderOpacity: number = 1;

  isLoggedIn = false; // Trạng thái đăng nhập
  userName = ''; // Lưu tên user sau khi đăng nhập
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

  // Header sẽ mờ dần khi cuộn xuống, không nhỏ hơn 0.7
  @HostListener('window:scroll', [])
  onWindowScroll() {
    const scrollTop = window.scrollY || document.documentElement.scrollTop;
    this.bgHeaderOpacity = Math.max(1 - scrollTop / 500, 0.7);
    // Chỉ làm mờ nền xuống tối thiểu 0.7, không ảnh hưởng nội dung
  }
}
