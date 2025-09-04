import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Params, Router } from '@angular/router';

@Component({
  selector: 'app-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss'],
})
export class LinkComponent implements OnInit {
  constructor(private router: Router) { }

  /**
   The Url of the link
  */
  @Input()
  href: string;

  @Input() disabled!: boolean;

  /**
   * The target of the link
   */
  @Input()
  target: string;

  /**
   The text of the link
  */
  @Input()
  text: string;

  /**
   * The component path to navigate from one component to another
   */
  @Input()
  link: string;
  /**
    Event fired when link is clicked
    If this is null, link should navigate to href
  */
  @Output()
  clicked = new EventEmitter<Event>();
  /**
   * Custom Classes for 'a' element
   */
  @Input()
  customClasses: string;
  /**
   * query parameters for the routerlink attribute
   */
  @Input() queryParams: Params = {};

  @Input() icon: string;
  @Input() tooltip: string='';

  ngOnInit(): void { }

  linkClicked(e: any) {
    this.clicked.emit(e);
    if (this.link !== undefined && this.link !== null && this.link !== "") {
      if (Object.values(this.queryParams).length === 0) {
        this.router.navigate([this.link]);
      }
      else {
        this.router.navigate([this.link], { queryParams: this.queryParams });
      }
    }
  }
}
