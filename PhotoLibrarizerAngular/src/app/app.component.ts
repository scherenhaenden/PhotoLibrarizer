import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {PhotoGalleryComponent} from "./views/photo-gallery/photo-gallery.component";
import {SidebarComponent} from "./views/sidebar/sidebar.component";
import {TopBarComponent} from "./views/top-bar/top-bar.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, PhotoGalleryComponent, SidebarComponent, TopBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'PhotoLibrarizerAngular';
}
