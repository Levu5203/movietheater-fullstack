import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  images = [
    '../assets/film1.jpg',
    '../assets/film2.jpg',
    '../assets/film3.jpg',
    '../assets/film4.jpg',
    '../assets/film5.jpg'
  ];

  public faPrevious: IconDefinition = faChevronLeft;
  public faNext: IconDefinition = faChevronRight;
  
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