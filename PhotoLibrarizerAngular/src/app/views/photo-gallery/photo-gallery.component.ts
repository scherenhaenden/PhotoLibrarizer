import {Component, OnInit} from '@angular/core';
import {NgForOf, NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-photo-gallery',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgForOf
  ],
  templateUrl: './photo-gallery.component.html',
  styleUrl: './photo-gallery.component.css'
})
export class PhotoGalleryComponent implements OnInit {




  public images = [
    {url: 'http://localhost:4200/assets/sample1.jpg', title: 'Sample Image 1'},
    {url: '/assets/sample2.jpg', title: 'Sample Image 2'},
    {url: '/assets/sample3.jpg', title: 'Sample Image 3'},
    // Add more images here
  ];

  constructor() {
  }

  ngOnInit(): void {
  }

  // method to transform objs to json
  public toJson(obj: any): string {
    return JSON.stringify(obj);
  }
}
