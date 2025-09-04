import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildNavBarComponent } from './child-nav-bar.component';

describe('ChildNavBarComponent', () => {
  let component: ChildNavBarComponent;
  let fixture: ComponentFixture<ChildNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChildNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChildNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
