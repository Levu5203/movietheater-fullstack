import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCamera, IconDefinition } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-addpromotion',
  imports: [FontAwesomeModule],
  templateUrl: './addpromotion.component.html',
  styleUrl: './addpromotion.component.css'
})
export class AddpromotionComponent {
  public faCamera: IconDefinition = faCamera;
}
