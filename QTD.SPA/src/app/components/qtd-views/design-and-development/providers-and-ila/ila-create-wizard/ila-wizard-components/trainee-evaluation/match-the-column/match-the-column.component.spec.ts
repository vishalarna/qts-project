import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchTheColumnComponent } from './match-the-column.component';

describe('MatchTheColumnComponent', () => {
  let component: MatchTheColumnComponent;
  let fixture: ComponentFixture<MatchTheColumnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchTheColumnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchTheColumnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
