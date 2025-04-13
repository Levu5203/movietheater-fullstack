import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCamera, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MOVIE_SERVICE } from '../../../constants/injection.constant';
import { IMovieServiceInterface } from '../../../services/movie/movie-service.interface';

@Component({
  selector: 'app-addmovie',
  imports: [FontAwesomeModule, CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './addmovie.component.html',
  styleUrl: './addmovie.component.css'
})
export class AddmovieComponent implements OnInit {
  public faCamera: IconDefinition = faCamera;

  //#endregion
  public form!: FormGroup;
  public errorMessage: string = '';
  public showErrorMessage: boolean = false;

  @Input() public selectedItem!: MovieviewModel | undefined | null;
  @Output() close = new EventEmitter<void>();
  @Output() dataChanged = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }

  movie: any;

  constructor(
    @Inject(MOVIE_SERVICE) private readonly movieService: IMovieServiceInterface
  ) {}

  ngOnInit(): void {
    this.createForm();
    // this.updateForm();
  }

  public createForm() {
    this.form = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      duration: new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]{10,18}$'),
      ]),
      origin: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      description: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      director: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      actor: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      version: new FormControl('', [
        Validators.required,
      ]),
      posterUrl: new FormControl('', [
        Validators.required,
      ]),
      status: new FormControl('', [
        Validators.required,
      ]),
      releaseDate: new FormControl(null),
      genres: new FormControl([], [
        Validators.required,

      ]),


      username: new FormControl({ value: '', disabled: !!this.selectedItem }, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50),
      ]),
      dateOfBirth: new FormControl(null),
      gender: new FormControl('Male', [Validators.required]),
      email: new FormControl({ value: '', disabled: !!this.selectedItem }, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50),
        Validators.email,
      ]),
      identityCard: new FormControl('', [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(18),
        Validators.pattern('^[0-9]{10,18}$'),
      ]),
      phoneNumber: new FormControl(null, [
        Validators.pattern('/^[0-9]{10,15}$/'),
      ]),
      address: new FormControl(''),
    });
    this.form.valueChanges.subscribe(() => {
      this.showErrorMessage = false;
    });
    
  }
}
