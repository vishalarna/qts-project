import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShIlaComponent } from './sh-ila.component';

describe('ShIlaComponent', () => {
  let component: ShIlaComponent;
  let fixture: ComponentFixture<ShIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
