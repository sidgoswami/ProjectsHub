import { TestBed, fakeAsync } from "@angular/core/testing"
import { AppComponent } from "./app.component"
import { Router } from "@angular/router"
import { RouterTestingModule } from "@angular/router/testing"
import { Location } from '@angular/common';
import { routes } from "./app-routing.module";


describe('Routing Modules', () => {
    let router : Router;
    let location : Location;
    let fixture;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule.withRoutes(routes)],
            declarations: [
                AppComponent
            ]
        })

        router = TestBed.inject(Router);
        location = TestBed.inject(Location);
        fixture = TestBed.createComponent(AppComponent);

        router.initialNavigation();
    });

    it('should configure routes to be "3"', () => {        
        expect(routes.length).toBe(3);
    })

    it('should route to home page when navigating to ""', fakeAsync(() => {
        router.navigate(['']).then(() => {
            expect(location.path()).toBe('/home');
        });
    }))

    it('should route to "list" page', fakeAsync(() => {
        router.navigate(['/list']).then(() => {
            expect(location.path()).toBe('/list');
        });
    }))
})