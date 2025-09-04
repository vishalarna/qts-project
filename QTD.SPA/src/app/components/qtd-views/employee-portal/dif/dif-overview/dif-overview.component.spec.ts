import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifOverviewComponent } from './dif-overview.component';

describe('DifOverviewComponent', () => {
  let component: DifOverviewComponent;
  let fixture: ComponentFixture<DifOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
