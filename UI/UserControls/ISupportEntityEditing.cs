using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MemCacheDManager.UI.UserControls
{
	public interface ISupportEntityEditing
	{
		Business.EditableEntityBase EditableEntity { get; }

		bool DoesControlHaveUnsavedChanges();

		void Edit(Business.EditableEntityBase entityToEdit, Business.EditableEntityBase parentEntity);

		void CreateNew(Business.EditableEntityBase parentEntity);

		void SetFocusToDefaultControl();
	}
}
