///////////////////////////////////////////////////////////
//  GeneralOrdering.cs
//  Implementation of the Class GeneralOrdering
//  Generated by Enterprise Architect
//  Created on:      04-Oct-2018 16:51:31
//  Original author: Iva
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using UML.Interactions;
using UML.CommonStructure;
namespace UML.Interactions {
	/// <summary>
	/// A GeneralOrdering represents a binary relation between two
	/// OccurrenceSpecifications, to describe that one OccurrenceSpecification must
	/// occur before the other in a valid trace. This mechanism provides the ability to
	/// define partial orders of OccurrenceSpecifications that may otherwise not have a
	/// specified order.
	/// </summary>
	public class GeneralOrdering : Element /*: NamedElement*/ { //TODO

		/// <summary>
		/// The OccurrenceSpecification referenced comes after the OccurrenceSpecification
		/// referenced by before.
		/// </summary>
		public UML.Interactions.OccurrenceSpecification after;
		/// <summary>
		/// The OccurrenceSpecification referenced comes before the OccurrenceSpecification
		/// referenced by after.
		/// </summary>
		public UML.Interactions.OccurrenceSpecification before;

		public GeneralOrdering(){

		}

		~GeneralOrdering(){

		}

	}//end GeneralOrdering

}//end namespace Interactions