import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCamera, IconDefinition } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-updatemovie',
  imports: [FontAwesomeModule],
  templateUrl: './updatemovie.component.html',
  styleUrl: './updatemovie.component.css'
})
export class UpdatemovieComponent {
  public faCamera: IconDefinition = faCamera;
}
