import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-moviemanagement',
  imports: [CommonModule],
  templateUrl: './moviemanagement.component.html',
  styleUrl: './moviemanagement.component.css'
})
export class MoviemanagementComponent {
  public movies = [
    { id: 1, name: "The Last Kingdom", releaseDate: "2022-05-15", company: "Netflix", duration: 126, version: "2D" },
    { id: 2, name: "Avatar: The Way of Water", releaseDate: "2022-12-16", company: "20th Century Studios", duration: 192, version: "3D" },
    { id: 3, name: "Inception", releaseDate: "2010-07-16", company: "Warner Bros.", duration: 148, version: "2D" },
    { id: 4, name: "Toy Story 4", releaseDate: "2019-06-21", company: "Pixar", duration: 100, version: "3D" },
    { id: 5, name: "Interstellar", releaseDate: "2014-11-07", company: "Paramount Pictures", duration: 169, version: "2D" },
    { id: 6, name: "Frozen II", releaseDate: "2019-11-22", company: "Disney", duration: 103, version: "3D" },
    { id: 7, name: "The Dark Knight", releaseDate: "2008-07-18", company: "Warner Bros.", duration: 152, version: "2D" },
    { id: 8, name: "Spider-Man: Into the Spider-Verse", releaseDate: "2018-12-14", company: "Sony Pictures", duration: 117, version: "3D" },
    { id: 9, name: "The Matrix", releaseDate: "1999-03-31", company: "Warner Bros.", duration: 136, version: "2D" },
    { id: 10, name: "Coco", releaseDate: "2017-11-22", company: "Pixar", duration: 105, version: "3D" }
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
