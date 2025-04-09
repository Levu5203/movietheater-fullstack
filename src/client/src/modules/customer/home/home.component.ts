import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import { MOVIE_SERVICE } from '../../../constants/injection.constant';
import { IMovieServiceInterface } from '../../../services/movie/movie-service.interface';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { ServicesModule } from '../../../services/services.module';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FontAwesomeModule, ServicesModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends MasterDataListComponent<MovieviewModel>  implements OnInit{
  public movies: MovieviewModel[] = [];
  constructor(
    @Inject(MOVIE_SERVICE) private readonly movieService: IMovieServiceInterface,
  ) {
    super();
  }

  images = [
    '../assets/film1.jpg',
    '../assets/film2.jpg',
    '../assets/film3.jpg',
    '../assets/film4.jpg',
    '../assets/film5.jpg'
  ];
  
  promotions1 = [
    '../assets/km1.png',
    '../assets/km2.png',
    '../assets/km3.png',
    '../assets/km1.png'
  ];
  
  promotions2 = [
    '../assets/km2.png',
    '../assets/km3.png',
    '../assets/km1.png'
  ];

  public faPrevious: IconDefinition = faChevronLeft;
  public faNext: IconDefinition = faChevronRight;
  
  currentIndex = 0;
  interval: any;

  override ngOnInit() {
    this.getAll();
    this.startSlideshow();

  }
  getAll(): void {
    this.movieService.getAll().subscribe((res) => {
      this.movies = res.filter(movie => movie.showtimes && movie.showtimes.length > 0);
    });
  
  }


  startSlideshow() {
    this.interval = setInterval(() => {
      this.nextSlide();
    }, 3000);
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
