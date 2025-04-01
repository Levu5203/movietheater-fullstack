import { Component, HostListener } from '@angular/core';
import { NavigationStart, Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'client';

  private isNavigating = false;

  constructor(private readonly router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.isNavigating = true;
      }
    });
  }

  @HostListener('window:beforeunload')
  onBeforeUnload() {
    if (!this.isNavigating) {
      localStorage.removeItem('token');
    }
    this.isNavigating = false;
  }
}
