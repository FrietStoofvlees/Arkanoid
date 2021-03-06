
package com.lawajo.arkanoid.view;

import com.lawajo.arkanoid.model.SliderModel;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;


/**
 * @author Lander Ketelbuters, Joey Koster, Ward Vanmuysen
 */
public class SliderView extends ViewObject {
    
    //Datamembers
    private final SliderModel model;
    private final Rectangle rect;
    
    
    /**
     * Initializes a new slider.
     * 
     * @param model The model which controls the slider.
     */
    public SliderView(SliderModel model) {
        this.model = model;
        
        this.rect = new Rectangle(this.model.WIDTH, this.model.HEIGHT);
        this.rect.setFill(Color.CHOCOLATE);
        this.rect.setStroke(Color.BLACK);
        this.rect.setStrokeWidth(0.4);
        
        this.getChildren().add(this.rect);
    }
    
    
    /**
     * Updates the current slider object with the newest parameters from the model.
     */
    @Override
    public void update() {
        this.rect.setLayoutX(this.model.getX());
        this.rect.setLayoutY(this.model.getY());
        this.rect.setFill(Color.CHOCOLATE);
    }
    
    
    /**
     * Gets the model of the boost.
     * 
     * @return The slidermodel that controls the sliderview.
     */
    @Override
    public SliderModel getModel() {
        return this.model;
    }
    
}
