import { analyzeArray } from './arrayAnalyzer.js'
import { expect } from 'chai'

describe('Test of arrayAnalyzer function', ()=> {
    it('should return undefined if given a string as input', ()=>{
        // Arrange
        const input = 'string';
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.be.undefined
    })
    it('should return undefined if given a number as input', ()=>{
        // Arrange
        const input = 12;
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.be.undefined
    })
    it('should return undefined if given a undefined as input', ()=>{
        // Arrange
        const input = undefined;
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.be.undefined
    })
    it('should return undefined if given a object as input', ()=>{
        // Arrange
        let input = {
            brand: 'Toyota',
            model: 'Camry',
            year: 2020,
            features: ['automatic transmission', 'air conditioning']
          };
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.be.undefined
    })
    it('should return undefined if given a empty array as input', ()=>{
        // Arrange
        let input = [];
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.be.undefined
    })
    it('should return undefined if given a array of strings as input',()=> {
          // Arrange
          let input = ['uno','dos','tres'];
          // Act
          let result = analyzeArray(input);
          // Assert
          expect(result).to.be.undefined
    })
    it('should return undefined if given a array of string as input',()=> {
        // Arrange
        let input = ['uno'];
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.be.undefined
    })
    it('should return undefined if given a array of numbers as strings as input',()=> {
        // Arrange
        let input = ['9','4','6'];
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.be.undefined
    })
    it('should return correct result if given a array of single number as input',()=> {
        // Arrange
        let input = [5];
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.deep.equal({ min: 5, max: 5, length: 1 })
    })
    it('should return correct result if given a array of equals numbers as input',()=> {
        // Arrange
        let input = [6,6,6,6];
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.deep.equal({ min: 6, max: 6, length: 4 })
    })
    it('should return correct result if given a array of numbers as input',()=> {
        // Arrange
        let input = [1,2,3,6];
        // Act
        let result = analyzeArray(input);
        // Assert
        expect(result).to.deep.equal({ min: 1, max: 6, length: 4 })
    })
})