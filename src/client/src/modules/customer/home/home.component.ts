import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faChevronLeft,
  faChevronRight,
} from '@fortawesome/free-solid-svg-icons';
import { MOVIE_SERVICE } from '../../../constants/injection.constant';
import { IMovieServiceInterface } from '../../../services/movie/movie-service.interface';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { ServicesModule } from '../../../services/services.module';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { MoviedetailComponent } from '../moviedetail/moviedetail.component';
import { PromotionModel } from '../../../models/promotion/promotion.model';
import { HttpClient } from '@angular/common/http';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [
    CommonModule,
    FontAwesomeModule,
    ServicesModule,
    MoviedetailComponent,
    RouterLink
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent
  extends MasterDataListComponent<MovieviewModel>
  implements OnInit {
  public movies: MovieviewModel[] = [];
  public nowShowingMovies: MovieviewModel[] = [];
  public comingSoonMovies: MovieviewModel[] = [];
  public promotions: PromotionModel[] = [];
  constructor(
    @Inject(MOVIE_SERVICE)
    private readonly movieService: IMovieServiceInterface,
    private http: HttpClient
  ) {
    super();
  }

  public selectMovie(id: string): void {
    setTimeout(() => {
      this.selectedItem = this.movies.find((x) => x.id === id);
      this.isShowDetail = true;

      // Scroll into view
    }, 150);
  }
  promotions1 = [
    '../assets/km1.png',
    '../assets/km2.png',
    '../assets/km3.png',
    '../assets/km1.png',
  ];

  promotions2 = ['../assets/km2.png', '../assets/km3.png', '../assets/km1.png'];

  public faPrevious: IconDefinition = faChevronLeft;
  public faNext: IconDefinition = faChevronRight;

  currentIndex = 0;
  interval: any;
  isTransitioning: boolean = false;

  override ngOnInit() {
    this.getAll();
    this.fetchRoom();
  }
  ngOnDestroy(): void {
    this.stopSlideshow();
  }

  getAll(): void {
    this.movieService.getAll().subscribe((res) => {
      this.movies = res;
      const now = new Date();
      this.nowShowingMovies = res.filter(
        (movie) =>
          movie.showtimes && movie.showtimes.length > 0 && movie.showtimes.some((showtime) => new Date(`${showtime.showDate}T${showtime.startTime}`) > now) && movie.status == 1
      );
      this.comingSoonMovies = res.filter(
        (movie) => movie.showtimes && movie.status == 2
      );

      console.log(this.movies);

      if (this.movies.length > 0) {
        this.startSlideshow();
      }
    });
  }

  getPrevMovie() {
    return this.nowShowingMovies.length
      ? this.nowShowingMovies[
      (this.currentIndex - 1 + this.nowShowingMovies.length) % this.nowShowingMovies.length
      ]
      : null;
  }

  getNextMovie() {
    return this.nowShowingMovies.length
      ? this.nowShowingMovies[
      (this.currentIndex + 1) % this.nowShowingMovies.length
      ]
      : null;
  }

  startSlideshow() {
    if (this.nowShowingMovies.length === 0) return;

    this.interval = setInterval(() => {
      this.nextSlide();
    }, 5000);
  }

  nextSlide() {
    if (this.isTransitioning) return;
    this.isTransitioning = true;
    this.currentIndex = (this.currentIndex + 1) % this.nowShowingMovies.length;
    this.stopSlideshow();
    this.startSlideshow();
    setTimeout(() => (this.isTransitioning = false), 500);
  }

  prevSlide() {
    if (this.isTransitioning) return;
    this.isTransitioning = true;
    this.currentIndex =
      (this.currentIndex - 1 + this.nowShowingMovies.length) %
      this.nowShowingMovies.length;
    this.stopSlideshow();
    this.startSlideshow();
    setTimeout(() => (this.isTransitioning = false), 500);
  }

  stopSlideshow() {
    clearInterval(this.interval);
  }
  //get promotions fromt api http://localhost:5063/api/Promotion
  private fetchRoom() {
    this.http
      .get<PromotionModel[]>(`http://localhost:5063/api/Promotion`)
      .subscribe((response: PromotionModel[]) => {
        this.promotions = response;
        console.log('Promotion data:', this.promotions);
      });
  }
}
