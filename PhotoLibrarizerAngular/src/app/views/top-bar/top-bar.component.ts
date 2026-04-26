import {Component, Input} from '@angular/core';
import {CommonModule, NgIf} from "@angular/common";

@Component({
  selector: 'app-top-bar',
  standalone: true,
  imports: [
    NgIf,
    CommonModule
  ],
  templateUrl: './top-bar.component.html',
  styleUrl: './top-bar.component.css'
})
export class TopBarComponent {
  @Input() title: string = 'Default Title';
  @Input() showSearch: boolean = true;
}
