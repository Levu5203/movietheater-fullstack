import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faRotateLeft, faFilter, faEdit, faTrash, faInfoCircle, faAngleRight, faAngleLeft, faAnglesLeft, faAnglesRight } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-moviemanagement',
  imports: [CommonModule, FontAwesomeModule, FormsModule],
  templateUrl: './moviemanagement.component.html',
  styleUrl: './moviemanagement.component.css'
})
export class MoviemanagementComponent {
  public faArrowLeft: IconDefinition = faArrowLeft;
    public faRotateLeft: IconDefinition = faRotateLeft;
    public faFilter: IconDefinition = faFilter;
    public faEdit: IconDefinition = faEdit;
    public faTrash: IconDefinition = faTrash;
    public faInfoCircle: IconDefinition = faInfoCircle;
    public faAngleRight: IconDefinition = faAngleRight;
    public faAngleLeft: IconDefinition = faAngleLeft;
    public faAnglesLeft: IconDefinition = faAnglesLeft;
    public faAnglesRight: IconDefinition = faAnglesRight;

    public isDropdownOpen: boolean = false;

    public toggleDropdown(): void {
      this.isDropdownOpen = !this.isDropdownOpen;
    }

  public movies = [
    {
      "Id": 1,
      "Name": "Inception",
      "Origin": "USA",
      "Description": "A mind-bending thriller by Christopher Nolan.",
      "Version": "IMAX",
      "PosterUrl": "https://example.com/inception.jpg",
      "Status": "Released",
      "ReleaseDate": "2010-07-16",
      "Director": "Christopher Nolan",
      "Actors": "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
      "Duration": 148
    },
    {
      "Id": 2,
      "Name": "Parasite",
      "Origin": "South Korea",
      "Description": "A dark comedy thriller about class struggle.",
      "Version": "2D",
      "PosterUrl": "https://example.com/parasite.jpg",
      "Status": "Released",
      "ReleaseDate": "2019-05-30",
      "Director": "Bong Joon-ho",
      "Actors": "Song Kang-ho, Lee Sun-kyun, Cho Yeo-jeong",
      "Duration": 132
    },
    {
      "Id": 3,
      "Name": "Interstellar",
      "Origin": "USA",
      "Description": "A sci-fi epic about space exploration and time dilation.",
      "Version": "IMAX",
      "PosterUrl": "https://example.com/interstellar.jpg",
      "Status": "Released",
      "ReleaseDate": "2014-11-07",
      "Director": "Christopher Nolan",
      "Actors": "Matthew McConaughey, Anne Hathaway, Jessica Chastain",
      "Duration": 169
    },
    {
      "Id": 4,
      "Name": "The Dark Knight",
      "Origin": "USA",
      "Description": "A legendary Batman movie featuring the Joker.",
      "Version": "IMAX",
      "PosterUrl": "https://example.com/thedarkknight.jpg",
      "Status": "Released",
      "ReleaseDate": "2008-07-18",
      "Director": "Christopher Nolan",
      "Actors": "Christian Bale, Heath Ledger, Aaron Eckhart",
      "Duration": 152
    },
    {
      "Id": 5,
      "Name": "Avengers: Endgame",
      "Origin": "USA",
      "Description": "The final showdown between the Avengers and Thanos.",
      "Version": "3D",
      "PosterUrl": "https://example.com/endgame.jpg",
      "Status": "Released",
      "ReleaseDate": "2019-04-26",
      "Director": "Anthony Russo, Joe Russo",
      "Actors": "Robert Downey Jr., Chris Evans, Mark Ruffalo",
      "Duration": 181
    },
    {
      "Id": 6,
      "Name": "Titanic",
      "Origin": "USA",
      "Description": "A tragic love story set on the Titanic.",
      "Version": "2D",
      "PosterUrl": "https://example.com/titanic.jpg",
      "Status": "Released",
      "ReleaseDate": "1997-12-19",
      "Director": "James Cameron",
      "Actors": "Leonardo DiCaprio, Kate Winslet, Billy Zane",
      "Duration": 195
    },
    {
      "Id": 7,
      "Name": "Joker",
      "Origin": "USA",
      "Description": "A deep dive into the origins of the Joker.",
      "Version": "IMAX",
      "PosterUrl": "https://example.com/joker.jpg",
      "Status": "Released",
      "ReleaseDate": "2019-10-04",
      "Director": "Todd Phillips",
      "Actors": "Joaquin Phoenix, Robert De Niro, Zazie Beetz",
      "Duration": 122
    },
    {
      "Id": 8,
      "Name": "Your Name",
      "Origin": "Japan",
      "Description": "A beautiful anime movie about love and fate.",
      "Version": "2D",
      "PosterUrl": "https://example.com/yourname.jpg",
      "Status": "Released",
      "ReleaseDate": "2016-08-26",
      "Director": "Makoto Shinkai",
      "Actors": "Ryunosuke Kamiki, Mone Kamishiraishi",
      "Duration": 107
    },
    {
      "Id": 9,
      "Name": "Dune",
      "Origin": "USA",
      "Description": "A sci-fi epic about a desert planet and power struggles.",
      "Version": "IMAX",
      "PosterUrl": "https://example.com/dune.jpg",
      "Status": "Released",
      "ReleaseDate": "2021-10-22",
      "Director": "Denis Villeneuve",
      "Actors": "Timothée Chalamet, Rebecca Ferguson, Oscar Isaac",
      "Duration": 155
    },
    {
      "Id": 10,
      "Name": "Spirited Away",
      "Origin": "Japan",
      "Description": "A magical journey into a world of spirits.",
      "Version": "2D",
      "PosterUrl": "https://example.com/spiritedaway.jpg",
      "Status": "Released",
      "ReleaseDate": "2001-07-20",
      "Director": "Hayao Miyazaki",
      "Actors": "Rumi Hiiragi, Miyu Irino, Mari Natsuki",
      "Duration": 125
    }
  ];

  currentPage: number = 1;
  itemsPerPage: number = 7;
  paginatedMovies: any[] = [];

  constructor() {
    this.updatePagination();
  }

  updatePagination() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedMovies = this.movies.slice(startIndex, endIndex);
  }

  goToPage(page: number) {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePagination();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.movies.length / this.itemsPerPage);
  }
}
