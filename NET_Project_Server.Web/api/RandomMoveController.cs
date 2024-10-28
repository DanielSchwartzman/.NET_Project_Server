using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace NET_Project_Server.Web.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomMoveController : ControllerBase
    {
        [HttpPost("random-move")]
        public IActionResult GetRandomMove([FromBody] List<List<int>> availableMoves)
        {
            if (availableMoves == null || availableMoves.Count == 0)
            {
                return BadRequest("No moves available");
            }

            var randomMove = ChooseRandomMove(availableMoves);
            return Ok(new { pieceIndex = randomMove.pieceIndex, moveIndex = randomMove.moveIndex, move = randomMove.move });
        }

        private (int pieceIndex, int moveIndex, int move) ChooseRandomMove(List<List<int>> availableMoves)
        {
            Random random = new Random();

            // Select a random piece (index from the list)
            int pieceIndex = random.Next(availableMoves.Count);

            // Ensure the selected piece has available moves
            if (availableMoves[pieceIndex].Count == 0)
            {
                return ChooseRandomMove(availableMoves); // Retry if no moves for the selected piece
            }

            // Select a random move from the selected piece's move list
            int moveIndex = random.Next(availableMoves[pieceIndex].Count);

            // Get the selected move (4-digit number)
            int move = availableMoves[pieceIndex][moveIndex];

            return (pieceIndex, moveIndex, move);
        }
    }
}
