import { Component } from '@angular/core';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faCamera } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-updatepromotion',
  imports: [FontAwesomeModule],
  templateUrl: './updatepromotion.component.html',
  styleUrl: './updatepromotion.component.css'
})
export class UpdatepromotionComponent {
  public faCamera: IconDefinition = faCamera;
}
