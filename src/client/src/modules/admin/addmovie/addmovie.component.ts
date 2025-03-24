import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCamera, IconDefinition } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-addmovie',
  imports: [FontAwesomeModule],
  templateUrl: './addmovie.component.html',
  styleUrl: './addmovie.component.css'
})
export class AddmovieComponent {
  public faCamera: IconDefinition = faCamera;
}
