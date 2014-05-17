using GameOfLife.Entities;
using NUnit.Framework;
using System.Linq;

namespace GameOfLife.Tests
{
    public class SimulatorTests
    {
        [Test]
        public void Should_ReturnASingleDeadCell_When_InitiatingFromASingleDeadCell_And_RunningOneCycle()
        {
            var startingState = Cell.CreateDefaultMatrix(rows: 1, columns: 1);

            var simulator = new Simulator(startingState, 1);

            var simulation = simulator.Simulation;

            Assert.That(simulation.SelectMany(cells => cells).All(x => x.State == CellState.Dead), Is.True);
        }

        [Test]
        public void Should_ReturnASingleDeadCell_When_InitiatingFromASingleLiveCell_And_RunningOneCycle()
        {
            var startingState = Cell.CreateDefaultMatrix(rows: 1, columns: 1);

            startingState[0][0].State = CellState.Live;
            
            var simulator = new Simulator(startingState, 1);

            var simulation = simulator.Simulation;

            Assert.That(simulation.SelectMany(cells => cells).All(x => x.State == CellState.Dead), Is.True);
        }

        [Test]
        public void Should_ReturnAFourDeadCells_When_InitiatingFromFourDeadCells_And_RunningOneCycle()
        {
            var startingState = Cell.CreateDefaultMatrix(rows: 2, columns: 2);

            var simulator = new Simulator(startingState, 1);

            var simulation = simulator.Simulation;

            Assert.That(simulation.SelectMany(cells => cells).All(x => x.State == CellState.Dead), Is.True);
        }

        [Test]
        public void Should_ReturnAFourLiveCells_When_InitiatingFromThreeLiveCells_And_RunningOneCycle()
        {
            var startingState = Cell.CreateBlockMatrix();

            startingState[1][1].State = CellState.Live;

            var simulator = new Simulator(startingState, 1);

            var simulation = simulator.Simulation;

            AssertForBlockResult(simulation);
        }

        [Test]
        public void Should_ReturnAFourLiveCells_When_InitiatingFromFourLiveCells_And_RunningOneCycle()
        {
            var startingState = Cell.CreateBlockMatrix();

            var simulator = new Simulator(startingState, 1);

            var simulation = simulator.Simulation;

            AssertForBlockResult(simulation);
        }

        private static void AssertForBlockResult(Cell[][] simulation)
        {
            Assert.That(simulation[0][0].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[0][1].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[0][2].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[0][3].State, Is.EqualTo(CellState.Dead));

            Assert.That(simulation[1][0].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[2][0].State, Is.EqualTo(CellState.Dead));

            Assert.That(simulation[1][1].State, Is.EqualTo(CellState.Live));
            Assert.That(simulation[1][2].State, Is.EqualTo(CellState.Live));

            Assert.That(simulation[2][1].State, Is.EqualTo(CellState.Live));
            Assert.That(simulation[2][2].State, Is.EqualTo(CellState.Live));

            Assert.That(simulation[1][3].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[2][3].State, Is.EqualTo(CellState.Dead));

            Assert.That(simulation[3][0].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[3][1].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[3][2].State, Is.EqualTo(CellState.Dead));
            Assert.That(simulation[3][3].State, Is.EqualTo(CellState.Dead));
        }
    }
}
