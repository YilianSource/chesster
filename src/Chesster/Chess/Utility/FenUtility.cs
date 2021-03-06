﻿using System;
using System.Text;

namespace Chesster.Chess
{
    /// <summary>
    /// Provides utility to handle FEN notations.
    /// </summary>
    public static class FenUtility
    {
        /// <summary>
        /// The default position, as a FEN notation.
        /// </summary>
        public static string DefaultPosition { get; } = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        /// <summary>
        /// Converts the given FEN notation into a piece array, that can be used to create a <see cref="Board"/>.
        /// </summary>
        public static Piece[] FenToPieces(string fen)
        {
            try
            {
                Piece[] pieces = new Piece[64];
                fen = fen.Split(' ')[0];

                int rankIndex = 7;
                int fileIndex = 0;

                foreach (char c in fen)
                {
                    if (fileIndex >= 8)
                    {
                        rankIndex--;
                        fileIndex = 0;
                    }

                    if (c == '/')
                        continue;

                    if (char.IsDigit(c))
                        fileIndex += int.Parse(c.ToString());
                    else
                        pieces[rankIndex * 8 + fileIndex++] = c.ToPiece();
                }

                return pieces;
            }
            catch
            {
                throw new FormatException("Invalid FEN position.");
            }
        }
        /// <summary>
        /// Converts the specified piece array into a FEN string. Only includes the pieces, no castling rights or anything.
        /// </summary>
        public static string PiecesToFen(Piece[] pieces)
        {
            if (pieces.Length != 64)
                throw new ArgumentException("The array must contain 64 pieces, one for each square.");

            StringBuilder fen = new StringBuilder(8 * 8 + 7);

            int whitespaceCounter = 0;
            for (int rank = 7; rank >= 0; rank--)
            {
                for (int file = 0; file < 8; file++)
                {
                    Piece piece = pieces[rank * 8 + file];
                    if (piece != Piece.None)
                        fen.Append(piece.ToCharacter());
                    else
                        whitespaceCounter++;

                    if (whitespaceCounter > 0 && (file + 1 >= 8 || pieces[rank * 8 + file + 1] != Piece.None))
                    {
                        fen.Append(whitespaceCounter);
                        whitespaceCounter = 0;
                    }
                }

                if (rank > 0)
                    fen.Append('/');
            }

            return fen.ToString();
        }
    }
}
