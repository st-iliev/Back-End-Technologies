import {rgbToHexColor} from './rgbToHexColor.js';
import { expect } from 'chai';

describe('The function rgbToHexColor', () => {

    it('should return undefined if red color value is out of range', () => {
        // Arrange
        const green = 245;
        const blue = 100;
        // Act
        let nonNumericRedValue = rgbToHexColor(`red`,green,blue);
        let negativeRedValue = rgbToHexColor(-1,green,blue);
        let toBigRedValue = rgbToHexColor(256,green,blue);
        // Assert
        expect(nonNumericRedValue).to.be.undefined;
        expect(negativeRedValue).to.be.undefined;
        expect(toBigRedValue).to.be.undefined;

    })
    it('should return undefined if green color value is out of range', () => {
        // Arrange
        const red = 245;
        const blue = 100;
        // Act
        let nonNumericGreenValue = rgbToHexColor(red,'green',blue);
        let negativeGreenValue = rgbToHexColor(red,-598,blue);
        let toBigGreenValue = rgbToHexColor(red,2658,blue);
        // Assert
        expect(nonNumericGreenValue).to.be.undefined;
        expect(negativeGreenValue).to.be.undefined;
        expect(toBigGreenValue).to.be.undefined;

    })
    it('should return undefined if blue color value is out of range', () => {
        // Arrange
        const red = 158;
        const green = 199;
        // Act
        let nonNumericBlueValue = rgbToHexColor(red,green,'blue');
        let negativeBlueValue = rgbToHexColor(red,green,-69);
        let toBigBlueValue = rgbToHexColor(red,green,963);
        // Assert
        expect(nonNumericBlueValue).to.be.undefined;
        expect(negativeBlueValue).to.be.undefined;
        expect(toBigBlueValue).to.be.undefined;

    })
    it('should return a correct hex value if a correct rgb is given', () =>{
        const red = 100;
        const green = 101;
        const blue = 100;
        // Act
        let result = rgbToHexColor(red,green,blue);
        // Assert
        expect(result).to.be.equals('#646564');
    })
    it('should return a correct hex value if a max rgb value is given', () => {
        const red = 255;
        const green = 255;
        const blue = 255;
        // Act
        let result = rgbToHexColor(red,green,blue);
        // Assert
        expect(result).to.be.equals('#FFFFFF');
    })
    it('should return a correct hex value if a minimum rgb value is given', () => {
        const red = 0;
        const green = 0;
        const blue = 0;
        // Act
        let result = rgbToHexColor(red,green,blue);
        // Assert
        expect(result).to.be.equals('#000000');
    })
})