/***************************************************************************
 *                             SaveStrategy.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id: SaveStrategy.cs 844 2012-03-07 13:47:33Z mark $
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using Server;

namespace Server
{
	public abstract class SaveStrategy
	{
		public static SaveStrategy Acquire()
		{
			if (Core.MultiProcessor)
			{
				int processorCount = Core.ProcessorCount;

#if Framework_4_0
				if (processorCount > 2)
				{
					return new DynamicSaveStrategy();
				}
#else
				if (processorCount > 16)
				{
					return new ParallelSaveStrategy(processorCount);
				}
#endif
				else
				{
					return new DualSaveStrategy();
				}
			}
			else
			{
				return new StandardSaveStrategy();
			}
		}

		public abstract string Name { get; }
		public abstract void Save(SaveMetrics metrics, bool permitBackgroundWrite);

		public abstract void ProcessDecay();
	}
}