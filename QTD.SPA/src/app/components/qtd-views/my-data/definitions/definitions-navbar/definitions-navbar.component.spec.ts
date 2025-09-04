import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefinitionsNavbarComponent } from './definitions-navbar.component';

describe('DefinitionsNavbarComponent', () => {
  let component: DefinitionsNavbarComponent;
  let fixture: ComponentFixture<DefinitionsNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefinitionsNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DefinitionsNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
