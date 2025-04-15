import { CommonModule } from '@angular/common';
import { Component, Inject, Input, OnInit, SimpleChanges } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ServicesModule } from '../../../services/services.module';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { IMovieServiceInterface } from '../../../services/movie/movie-service.interface';
import { MOVIE_SERVICE } from '../../../constants/injection.constant';

@Component({
  selector: 'app-movies',
  imports: [RouterModule, CommonModule, ServicesModule],
  templateUrl: './movies.component.html',
  styleUrl: './movies.component.css',
})
export class MoviesComponent
  extends MasterDataListComponent<MovieviewModel>
  implements OnInit
{
  @Input() selectedShowtime!: Date;
  public movies: MovieviewModel[] = [];
  filteredMovies: MovieviewModel[] = [];
  private originalMovies: MovieviewModel[] = [];
  constructor(
    @Inject(MOVIE_SERVICE) private readonly movieService: IMovieServiceInterface
  ) {
    super();
  }
  override ngOnInit(): void {
    this.getAll();
  }
  private getAll(): void {
    this.movieService.getAll().subscribe((res) => {
      this.originalMovies = res.filter(
        (movie) => movie.showtimes && movie.showtimes.length > 0
      );
      this.movies = [...this.originalMovies]; // Sao chép danh sách gốc vào movies
    });
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes['selectedShowtime'] && this.selectedShowtime) {
      console.log('Received selectedShowtime:', this.selectedShowtime);
      if (this.movies.length > 0) {
        this.filterMovies(this.selectedShowtime);
      } else {
        // Đợi dữ liệu trả về rồi mới filter
        this.movieService.getAll().subscribe((res) => {
          this.movies = res.filter(
            (movie) => movie.showtimes && movie.showtimes.length > 0
          );
          this.filterMovies(this.selectedShowtime);
        });
      }
    }
  }
  filterMovies(selectedDate: Date) {
    if (!this.originalMovies.length) return;

    // Normalize selectedDate
    const selected = new Date(selectedDate);
    selected.setHours(0, 0, 0, 0);

    this.movies = this.originalMovies
      .map((movie) => {
        const filteredShowtimes = movie.showtimes.filter((showtime) => {
          const showDate = new Date(showtime.showDate);
          showDate.setHours(0, 0, 0, 0);
          return showDate.getTime() === selected.getTime();
        });

        return {
          ...movie,
          showtimes: filteredShowtimes,
        };
      })
      .filter((movie) => movie.showtimes.length > 0); // Keep only movies that have matching showtimes
  }
}
