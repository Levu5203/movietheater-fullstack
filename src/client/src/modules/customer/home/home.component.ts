import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  images = [
    '../assets/film1.jpg',
    '../assets/film2.jpg',
    '../assets/film3.jpg',
    '../assets/film4.jpg',
    '../assets/film5.jpg'
  ];
  
  currentIndex = 0;
  interval: any;

  constructor() {
    this.startSlideshow();
  }

  startSlideshow() {
    this.interval = setInterval(() => {
      this.nextSlide();
    }, 3000); // Tự động chuyển ảnh mỗi 3 giây
  }

  nextSlide() {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
  }

  prevSlide() {
    this.currentIndex = (this.currentIndex - 1 + this.images.length) % this.images.length;
  }

  stopSlideshow() {
    clearInterval(this.interval);
  }
}