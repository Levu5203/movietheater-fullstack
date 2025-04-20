import { Component, Inject, Input, OnInit, SimpleChanges } from '@angular/core';
import { MovieviewModel } from '../../../../models/movie/movieview.model';
import { MasterDataListComponent } from '../../../../core/components/master-data/master-data.component';
import { MOVIE_SERVICE } from '../../../../constants/injection.constant';
import { IMovieServiceInterface } from '../../../../services/movie/movie-service.interface';
import { ServicesModule } from '../../../../services/services.module';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CustomFormatPipe } from '../../../../pipes/custom-format.pipe';

@Component({
  selector: 'app-ticketselling-movies',
  imports: [ServicesModule, RouterLink, CommonModule, CustomFormatPipe],
  templateUrl: './ticketselling-movies.component.html',
  styleUrl: './ticketselling-movies.component.css',
})
export class TicketsellingMoviesComponent
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
        (movie) => movie.showtimes && movie.showtimes.length > 0 && movie.status === 1
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
        const filteredShowtimes = movie.showtimes
          .filter((showtime) => {
            const showDate = new Date(showtime.showDate);
            showDate.setHours(0, 0, 0, 0);
            return showDate.getTime() === selected.getTime();
          })
          .filter((x) => {
            return new Date(`${x.showDate}T${x.startTime}`) > new Date();
          }) // Filter out past showtimes
          .sort((a, b) => {
            const d1 = new Date(`${a.showDate}T${a.startTime}`);
            const d2 = new Date(`${b.showDate}T${b.startTime}`);
            return d1.getTime() - d2.getTime();
          }) ?? [];

        return {
          ...movie,
          showtimes: filteredShowtimes,
        };
      })
      .filter((movie) => movie.showtimes.length > 0); // Keep only movies that have matching showtimes
  }
}
