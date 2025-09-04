import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicRequestsComponent } from './public-requests.component';

describe('PublicRequestsComponent', () => {
  let component: PublicRequestsComponent;
  let fixture: ComponentFixture<PublicRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicRequestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
