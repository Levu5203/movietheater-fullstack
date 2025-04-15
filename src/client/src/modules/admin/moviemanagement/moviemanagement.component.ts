import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faRotateLeft, faFilter, faEdit, faTrash, faInfoCircle, faAngleRight, faAngleLeft, faAnglesLeft, faAnglesRight } from '@fortawesome/free-solid-svg-icons';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { MOVIE_ADMIN_SERVICE, MOVIE_SERVICE } from '../../../constants/injection.constant';
import { IMovieServiceInterface } from '../../../services/movie/movie-service.interface';
import { ServicesModule } from '../../../services/services.module';
import { TableComponent } from '../../../core/components/table/table.component';
import { TableColumn } from '../../../core/models/table-column.model';
import { AddmovieComponent } from '../addmovie/addmovie.component';
import { UpdatemovieComponent } from '../updatemovie/updatemovie.component';
import { MovieViewModel } from '../../../models/movie/movie-view-model';
import { IMovieAdminServiceInterface } from '../../../services/movieAdmin/movie-admin.interface';

@Component({
  selector: 'app-moviemanagement',
  imports: [CommonModule, FontAwesomeModule, FormsModule, ServicesModule, TableComponent,ReactiveFormsModule, UpdatemovieComponent ],
  templateUrl: './moviemanagement.component.html',
  styleUrl: './moviemanagement.component.css'
})
export class MoviemanagementComponent extends MasterDataListComponent<MovieViewModel> implements OnInit {
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

  public override columns: TableColumn[] = [
      { name: 'Name', value: 'name' },
      { name: 'Release Date', value: 'releasedDate', type: 'date' },
      { name: 'Director', value: 'director'},
      { name: 'Actors', value: 'actors' },
      { name: 'Duration', value: 'duration' },
      { name: 'Version', value: 'version' },
      ];

  constructor(@Inject(MOVIE_ADMIN_SERVICE) private readonly movieAdminService: IMovieAdminServiceInterface) {
    super();
  }

  protected override createForm(): void {
    this.searchForm = new FormGroup({
      keyword: new FormControl(''),
    });
    this.searchForm.valueChanges.subscribe(() => {
      this.onSubmit();
    });
  }

  protected override searchData(): void {
    this.movieAdminService.search(this.filter).subscribe((res) => {
      console.log(res);
      
      this.data = res;
    });
  }

  public delete(id: string): void {
    this.movieAdminService.delete(id).subscribe((data) => {
      // Neu xoa duoc thi goi lai ham getData de load lai du lieu
      if (data) {
        this.searchData();
      }
    });
  }
  public edit(id: string): void {
    this.isShowDetail = false;
    setTimeout(() => {
      this.selectedItem = this.data.items.find((x) => x.id === id);
      this.isShowForm = true;

      // Scroll into view
    }, 150);
  }

  public create(): void {
    this.isShowDetail = false;
    setTimeout(() => {
      this.selectedItem = null;
      this.isShowForm = true;

      // Scroll into view
    }, 150);
  }

  public view(id: string): void {
    this.isShowForm = false;
    setTimeout(() => {
      this.selectedItem = this.data.items.find((x) => x.id === id);
      this.isShowDetail = true;

      // Scroll into view
    }, 150);
  }

}
