import {Component, Input} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";

export interface MenuItem {
  label: string;
  icon: string;
  isActive: boolean;
  children?: MenuItem[]; // Optional array of child menu items
}


@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,

  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  menuItems: MenuItem[] = [
    {
      label: 'Search',
      icon: 'fas fa-search',
      isActive: true,
      children: [
        { label: 'Monochrome', icon: '', isActive: true },
        { label: 'Panoramas', icon: '', isActive: true },
        { label: 'Animated', icon: '', isActive: true },
        { label: 'Stacks', icon: '', isActive: true },
        { label: 'Scans', icon: '', isActive: true },
        { label: 'Review', icon: '', isActive: true },
        { label: 'Archive', icon: '', isActive: true },
      ]
    },
    { label: 'Albums', icon: 'fas fa-bookmark', isActive: true },
    {
      label: 'Videos',
      icon: 'fas fa-video',
      isActive: true,
      children: [
        { label: 'Video 1', icon: '', isActive: true },
        { label: 'Video 2', icon: '', isActive: true },
      ]
    },
    { label: 'People', icon: 'fas fa-user', isActive: true },
    { label: 'Favorites', icon: 'fas fa-heart', isActive: true },
    { label: 'Moments', icon: 'fas fa-clock', isActive: true },
    { label: 'Calendar', icon: 'fas fa-calendar-alt', isActive: true },
    {
      label: 'Places',
      icon: 'fas fa-map-marker-alt',
      isActive: true,
      children: [
        { label: 'Place 1', icon: '', isActive: true },
        { label: 'Place 2', icon: '', isActive: true },
      ]
    },
    { label: 'Labels', icon: 'fas fa-tags', isActive: true },
    { label: 'Folders', icon: 'fas fa-folder', isActive: true },
    { label: 'Private', icon: 'fas fa-lock', isActive: true },
    {
      label: 'Library',
      icon: 'fas fa-book',
      isActive: true,
      children: [
        { label: 'Library 1', icon: '', isActive: true },
        { label: 'Library 2', icon: '', isActive: true },
      ]
    },
    { label: 'Settings', icon: 'fas fa-cog', isActive: true },
    { label: 'Support Our Mission', icon: 'fas fa-info-circle', isActive: true }
  ];

  constructor() { }

  ngOnInit(): void { }

  toggleMenuItem(index: number): void {
    //this.menuItems[index].isActive = !this.menuItems[index].isActive;
  }

}
